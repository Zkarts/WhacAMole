using UnityEngine;
using System.Collections;

public class DefaultSlotDefinitionGenerator : MonoBehaviour, ISlotDefinitionGenerator {

    public RoundDefinition.SlotDefinition GenerateSlotDefinition(TargetTypeCollection targetTypeCollection, float weightSum) {
        RoundDefinition.SlotDefinition slotDefinition = new RoundDefinition.SlotDefinition();

        //use the weights to select the targetType
        float weightSelection = Random.Range(0, weightSum);
        float localSum = 0;
        for (int i = 0; i < targetTypeCollection.targetTypes.Count; i++) {
            localSum += targetTypeCollection.targetTypes[i].Weight;
            if (weightSelection < localSum) {
                slotDefinition.targetTypeIndex = i;
                break;
            }
        }

        slotDefinition.timeBeforePresent = 0;
        return slotDefinition;
    }

}
