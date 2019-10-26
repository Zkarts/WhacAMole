using UnityEngine;
using System.Collections;

public interface ISlotDefinitionGenerator {

    RoundDefinition.SlotDefinition GenerateSlotDefinition(TargetTypeCollection targetTypeCollection, float weightSum);

}
