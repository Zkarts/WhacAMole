using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IRoundDefinitionGenerator {

    void Init(TargetTypeCollection targetTypeCollection, float roundDuration);
    RoundDefinition GenerateRound(int roundNumber, int numberOfAvailableSlots);
    RoundDefinition GenerateEmptyRound(int numberOfAvailableSlots, int duration);
    RoundDefinition GenerateFinalRound(int numberOfAvailableSlots);

}
