using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICameraRayProvider {

    void SetCamera(Camera cam);
    Ray GetRay(int id);

}