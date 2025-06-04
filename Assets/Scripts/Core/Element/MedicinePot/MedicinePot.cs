using System.Collections;
using System.Collections.Generic;
using DIALOGUE;
using UnityEngine;
using UnityEngine.Splines;

public class MedicinePot : MonoBehaviour
{
    private List<string> targetMedicines = new List<string>();
    private HashSet<string> clickedMedicines = new HashSet<string>();
    private string OnDestoryScript;

    private Animator animator;
    private SplineAnimate splineAnimate;

    [SerializeField] private string resetAnimationName = "MedicinePot_Reset";
    private bool isSelecting = true;
    private Transform medicinesParent;
    private Dictionary<string, Transform> medicineNodes = new Dictionary<string, Transform>();

    private void Start()
    {
        animator = GetComponent<Animator>();
        splineAnimate = GetComponent<SplineAnimate>();
    }

    public void Initialize(string[] targetMedicineNames, string[] allMedicinePrefabs, string OnDestoryScriptID)
    {
        OnDestoryScript = OnDestoryScriptID;

        targetMedicines = new List<string>(targetMedicineNames);
        clickedMedicines.Clear();
        isSelecting = true;
        medicineNodes.Clear();

        medicinesParent = transform.Find("root")?.Find("Medicines");
        if (medicinesParent != null)
        {
            for (int i = 0; i < medicinesParent.childCount; i++)
            {
                Transform child = medicinesParent.GetChild(i);
                medicineNodes[child.name] = child;
            }

            for (int i = 0; i < allMedicinePrefabs.Length; i++)
            {
                if (i < medicinesParent.childCount)
                {
                    string nodeName = medicinesParent.GetChild(i).name;
                    string prefabName = allMedicinePrefabs[i];
                    SetupMedicinePrefab(nodeName, prefabName);
                }
            }
        }
        else
        {
            Debug.LogError("�Ҳ���Medicines���ڵ�");
        }
    }

    private void SetupMedicinePrefab(string nodeName, string prefabName)
    {
        if (!medicineNodes.TryGetValue(nodeName, out Transform node))
        {
            Debug.LogWarning($"�Ҳ���ҩƷ�ڵ�: {nodeName}");
            return;
        }

        foreach (Transform child in node)
        {
            Destroy(child.gameObject);
        }

        string prefabPath = $"Items/Medicine/{prefabName}/Medicine - [{prefabName}]";
        GameObject prefab = Resources.Load<GameObject>(prefabPath);

        if (prefab != null)
        {
            GameObject instance = Instantiate(prefab, node);
            instance.name = prefabName;
            instance.transform.localPosition = Vector3.zero;
            instance.transform.localRotation = Quaternion.identity;
            instance.transform.localScale = Vector3.one;

            AddClickHandler(instance.transform, nodeName);
        }
        else
        {
            Debug.LogWarning($"�޷�����ҩƷ {prefabName} ��Ԥ����");
        }
    }

    private void AddClickHandler(Transform node, string nodeName)
    {
        if (node == null)
        {
            Debug.LogWarning($"�޷�Ϊ {nodeName} ��ӵ�����������ڵ�Ϊ��");
            return;
        }

        MedicineClickHandler handler = node.GetComponent<MedicineClickHandler>();
        if (handler == null)
        {
            handler = node.gameObject.AddComponent<MedicineClickHandler>();
        }

        handler.Initialize(nodeName, (string clickedNodeName) =>
        {
            if (!isSelecting) return;

            bool isTargetMedicine = targetMedicines.Contains(clickedNodeName);

            if (isTargetMedicine)
            {
                if (!clickedMedicines.Contains(clickedNodeName))
                {
                    clickedMedicines.Add(clickedNodeName);
                    Debug.Log($"{clickedNodeName} �ѱ�ѡ��");

                    if (clickedMedicines.Count == targetMedicines.Count)
                    {
                        StartBrewing();
                    }
                }
            }
            else
            {
                Debug.Log($"{clickedNodeName} �ǲ���Ҫ��ҩƷ������ѡ��");
                StartCoroutine(ResetSelection());
            }
        });
    }

    private IEnumerator ResetSelection()
    {
        isSelecting = false;
        clickedMedicines.Clear();

        if (!string.IsNullOrEmpty(resetAnimationName))
        {
            animator.Play(resetAnimationName);
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
        }

        Debug.Log("ѡ�������ã�������ѡ��");
        isSelecting = true;
    }

    private void StartBrewing()
    {
        Debug.Log("���б�ҪҩƷ��ѡ�񣬿�ʼ��ҩ");
        isSelecting = false;
        animator.Play("MedicinePot_PutMedIn");

    }

    private void DestoryThePot()
    {
        CommandManager.instance.Execute("StartDialogue", "-f", OnDestoryScript.ToString());
        gameObject.SetActive(false);

        Destroy(gameObject);
    }
}