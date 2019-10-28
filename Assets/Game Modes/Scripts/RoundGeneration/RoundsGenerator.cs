using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoundsGenerator : MonoBehaviour {

    //Use a constant seed to keep different playthroughs comparable in highscores
    const int seed = 759172556;

    [SerializeField]
    float roundDuration = 3f;

    [SerializeField]
    private TargetTypeCollection targetTypeCollection;

    private IRoundDefinitionGenerator roundDefinitionGenerator;

    public TargetTypeCollection TargetTypeCollection => targetTypeCollection;

    private void Awake() {
        roundDefinitionGenerator = GetComponent<IRoundDefinitionGenerator>();
    }

    public List<RoundDefinition> GenerateRounds(float gameDurationInSeconds, int numberOfAvailableSlots) {
        if (numberOfAvailableSlots <= 0) {
            Debug.LogError("Need at least one slot to GenerateRounds");
            return null;
        }
        if (gameDurationInSeconds < roundDuration) {
            Debug.LogError("Game should take at least one roundDuration's worth of time");
            return null;
        }

        roundDefinitionGenerator.Init(targetTypeCollection, roundDuration);

        //rounded down to not have any shorter rounds, used as wait time for final round
        int numberOfRounds = (int)(gameDurationInSeconds / roundDuration);
        //without the final round
        int numberOfRandomRounds = numberOfRounds - 1;

        Random.InitState(seed);

        List<RoundDefinition> result = new List<RoundDefinition>(numberOfRounds);

        //generate all but the last round
        for (int i = 0; i < numberOfRandomRounds; i++) {
            result.Add(roundDefinitionGenerator.GenerateRound(i + 1, numberOfAvailableSlots));
        }

        //Fill seconds before the final round with an empty round
        int remainder = (int)(gameDurationInSeconds % roundDuration);
        if (remainder > 0) {
            result.Add(roundDefinitionGenerator.GenerateEmptyRound(numberOfAvailableSlots, remainder));
        }

        result.Add(roundDefinitionGenerator.GenerateFinalRound(numberOfAvailableSlots));

        return result;
    }

}
