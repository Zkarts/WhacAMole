using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoundDefinition {

    //The contents of a slot in the current round
    public class SlotDefinition {
        public int targetTypeIndex;
        public float timeBeforePresent;
    }

    public float duration;
    //allows for aligning the last round with the end of the game, building last minute tension
    public bool isFinalRound;
    //indices match the actual slots
    public List<SlotDefinition> orderedSlotDefinitions;

}
