using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoundsGenerator : MonoBehaviour {

    //Use a constant seed to keep different playthroughs comparable in highscores
    const int seed = 759172556;

    [SerializeField]
    float roundDuration = 3f;

    [SerializeField]
    [Tooltip("The round number multiplied by this factor makes how much percent of the slots are used (where 1.0 is 100%)")]
    float minimumSlotsFactor = 0.2f;

    [SerializeField]
    private TargetTypeCollection targetTypeCollection;

    public TargetTypeCollection TargetTypeCollection => targetTypeCollection;

    public List<RoundDefinition> GenerateRounds(float gameDurationInSeconds, int numberOfAvailableSlots) {
        if (numberOfAvailableSlots <= 0) {
            Debug.LogError("Need at least one slot to GenerateRounds");
            return null;
        }
        if (gameDurationInSeconds < roundDuration) {
            Debug.LogError("Game should take at least one roundDuration's worth of time");
            return null;
        }

        //rounded down to not have any shorter rounds, used as wait time for final round
        int numberOfRounds = (int)(gameDurationInSeconds / roundDuration);
        //without the final round
        int numberOfRandomRounds = numberOfRounds - 1;

        Random.InitState(seed);

        List<RoundDefinition> result = new List<RoundDefinition>(numberOfRounds);

        //generate all but the last round
        for (int i = 0; i < numberOfRandomRounds; i++) {
            result.Add(GenerateRound(i + 1, numberOfAvailableSlots));
        }

        result.Add(GenerateFinalRound(numberOfAvailableSlots));

        return result;
    }

    private RoundDefinition GenerateRound(int roundNumber, int numberOfAvailableSlots) {
        RoundDefinition result = new RoundDefinition();
        result.duration = roundDuration;
        result.isFinalRound = false;

        result.orderedSlotDefinitions = new List<RoundDefinition.SlotDefinition>(numberOfAvailableSlots);

        int minimumSlotsToUse = (int)(Mathf.Clamp01(minimumSlotsFactor * roundNumber) * numberOfAvailableSlots);
        int slotsToUse = Random.Range(Mathf.Max(1, minimumSlotsToUse), numberOfAvailableSlots);

        int[] indices = new int[numberOfAvailableSlots];
        for (int i = 0; i < numberOfAvailableSlots; i++) {
            indices[i] = i;
        }

        SelectIndicesToUse(ref indices, slotsToUse, numberOfAvailableSlots);

        for (int k = 0; k < numberOfAvailableSlots; k++) {
            bool useSlot = false;

            for (int l = 0; l < numberOfAvailableSlots; l++) {
                if (indices[l] == k) {
                    useSlot = l < slotsToUse;
                    break;
                }
            }

            if (useSlot) {
                RoundDefinition.SlotDefinition slotDefinition = new RoundDefinition.SlotDefinition();
                slotDefinition.targetTypeIndex = Random.Range(0, targetTypeCollection.targetTypes.Count - 1);
                slotDefinition.timeBeforePresent = 0;

                result.orderedSlotDefinitions.Add(slotDefinition);
            }
            else {
                result.orderedSlotDefinitions.Add(null);
            }
        }

        return result;
    }

    private void SelectIndicesToUse(ref int[] indices, int slotsToUse, int availableSlots) {
        Random.State generationState = Random.state;
        //use actual random for which slots to use this game
        Random.InitState(System.DateTime.Now.Millisecond);

        //Fisher-Yates shuffle; sets the first _slotsToUse_ indices to the picks efficiently
        for (int i = 0; i < slotsToUse; i++) {
            int pick = Random.Range(0, availableSlots);

            int swap = indices[pick];
            indices[pick] = indices[i];
            indices[i] = swap;
        }

        Random.state = generationState;
    }

    //Always fill all slots in the final round
    private RoundDefinition GenerateFinalRound(int numberOfAvailableSlots) {
        RoundDefinition result = new RoundDefinition();
        result.duration = roundDuration;
        result.isFinalRound = true;

        result.orderedSlotDefinitions = new List<RoundDefinition.SlotDefinition>();

        for (int i = 0; i < numberOfAvailableSlots; i++) {
            RoundDefinition.SlotDefinition slotDefinition = new RoundDefinition.SlotDefinition();
            slotDefinition.targetTypeIndex = Random.Range(0, targetTypeCollection.targetTypes.Count - 1);
            slotDefinition.timeBeforePresent = 0;

            result.orderedSlotDefinitions.Add(slotDefinition);
        }

        return result;
    }

}
