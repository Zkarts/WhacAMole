using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DefaultRoundDefinitionGenerator : MonoBehaviour, IRoundDefinitionGenerator {

    private ISlotDefinitionGenerator slotDefinitionGenerator;

    private float roundDuration;
    private float weightSum;
    private TargetTypeCollection targetTypeCollection;

    private void Awake() {
        slotDefinitionGenerator = GetComponent<ISlotDefinitionGenerator>();
    }

    public void Init(TargetTypeCollection targetTypeCollection, float roundDuration) {
        this.targetTypeCollection = targetTypeCollection;
        this.roundDuration = roundDuration;

        weightSum = 0;
        foreach (WhackTarget targetType in targetTypeCollection.targetTypes) {
            weightSum += targetType.Weight;
        }
    }

    public RoundDefinition GenerateRound(int roundNumber, int numberOfAvailableSlots) {
        RoundDefinition result = new RoundDefinition();
        result.duration = roundDuration;
        result.isFinalRound = false;

        result.orderedSlotDefinitions = new List<RoundDefinition.SlotDefinition>(numberOfAvailableSlots);

        int slotsToUse = Random.Range(1, numberOfAvailableSlots);

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
                RoundDefinition.SlotDefinition slotDefinition = slotDefinitionGenerator.GenerateSlotDefinition(targetTypeCollection, weightSum);
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

    //Creates an empty round for tension building
    public RoundDefinition GenerateEmptyRound(int numberOfAvailableSlots, int duration) {
        RoundDefinition result = new RoundDefinition();
        result.duration = duration;
        result.isFinalRound = false;

        result.orderedSlotDefinitions = new List<RoundDefinition.SlotDefinition>();

        for (int i = 0; i < numberOfAvailableSlots; i++) {
            result.orderedSlotDefinitions.Add(null);
        }

        return result;
    }

    //Always fill all slots in the final round
    public RoundDefinition GenerateFinalRound(int numberOfAvailableSlots) {
        RoundDefinition result = new RoundDefinition();
        result.duration = roundDuration;
        result.isFinalRound = true;

        result.orderedSlotDefinitions = new List<RoundDefinition.SlotDefinition>();

        for (int i = 0; i < numberOfAvailableSlots; i++) {
            RoundDefinition.SlotDefinition slotDefinition = slotDefinitionGenerator.GenerateSlotDefinition(targetTypeCollection, weightSum);
            result.orderedSlotDefinitions.Add(slotDefinition);
        }

        return result;
    }

}
