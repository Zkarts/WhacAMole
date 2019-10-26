using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotsManager : MonoBehaviour {

    [SerializeField]
    private TargetController controller;

    [SerializeField]
    private TargetPresenter targetPresenterPrefab;

    [SerializeField]
    private Transform gameBoard;

    [Header("Hole size")]
    [SerializeField]
    private float holeWidth = 1f;

    [SerializeField]
    private float holeHeight = 1f;

    [Header("Slot grid")]
    [SerializeField]
    [Range(1, 3)]
    private int maxRows = 2;

    [SerializeField]
    [Range(1, 8)]
    private int maxCols = 4;

    public int MaxRows => maxRows;
    public int MaxCols => maxCols;

    public void GenerateTargets(int numberOfSlots) {
        //clean up old children
        foreach (Transform child in controller.transform) {
            Destroy(child.gameObject);
        }

        if (numberOfSlots > maxRows * maxCols) {
            Debug.LogError("Requesting invalid number of slots, more than maximum set");
            return;
        }

        int rows = Mathf.Min(maxRows, numberOfSlots);

        //Fill rows from the top
        int[] colsPerRow = new int[rows];
        for (int i = 0; i < rows; i++) {
            colsPerRow[i] = numberOfSlots / maxRows + (numberOfSlots % maxRows > i ? 1 : 0);
        }

        float halfXPos = 0.5f * gameBoard.lossyScale.x;
        float halfZPos = 0.5f * gameBoard.lossyScale.z;

        List<TargetPresenter> presenters = new List<TargetPresenter>();

        for (int i = 0; i < numberOfSlots; i++) {
            int row = i % maxRows;
            int col = i / maxRows;

            TargetPresenter presenter = GameObject.Instantiate<TargetPresenter>(targetPresenterPrefab, controller.transform);
            //Center the slots
            Vector3 position = presenter.transform.localPosition;
            position.x = (row + 0.5f) * gameBoard.lossyScale.x / rows - halfXPos;
            position.z = (col + 0.5f) * gameBoard.lossyScale.z / colsPerRow[row] - halfZPos;
            presenter.transform.localPosition = position;

            presenters.Add(presenter);
        }

        controller.SetPresenters(presenters);
    }

}
