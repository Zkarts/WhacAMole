using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectionService {

    void Refresh();
    bool DetectSelection(out int id);

}