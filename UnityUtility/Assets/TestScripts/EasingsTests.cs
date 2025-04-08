using System;

using UnityEngine;

using UnityUtility.Easings;
using UnityUtility.Extensions;
using UnityUtility.MathU;
using UnityUtility.Pools;
using UnityUtility.Timer;

public class EasingsTests : MonoBehaviour
{
    [SerializeField] private ComponentPool<Transform> m_transformPool;
    [SerializeField] private Timer m_easingTimer;
    [SerializeField] private Vector2 m_movementXRange;

    [NonSerialized] private Transform[] m_pooledTransforms;

    // Start is called before the first frame update
    private void Start()
    {
        m_pooledTransforms = new Transform[(int)Easings.EasingFunction.Count];
        for (int iFunction = 0; iFunction < (int)Easings.EasingFunction.Count; ++iFunction)
        {
            Transform pooledTransform = m_transformPool.Request().Object;
            pooledTransform.gameObject.name = $"{(Easings.EasingFunction)iFunction}";
            pooledTransform.parent = transform;
            pooledTransform.gameObject.SetActive(true);
            transform.localPosition = new Vector3(0.0f, iFunction * 3.0f, 0.0f);
            m_pooledTransforms[iFunction] = pooledTransform;
        }

        m_easingTimer.Start();
    }

    // Update is called once per frame
    private void Update()
    {
        _ = m_easingTimer.Update(Time.deltaTime);
        float t = MathUf.Abs(m_easingTimer.Progress.RemapFrom01(-1.0f, 1.0f));
        Debug.Log(t);

        for (int iFunction = 0; iFunction < (int)Easings.EasingFunction.Count; ++iFunction)
        {
            m_pooledTransforms[iFunction].localPosition = new Vector3(Easings.Ease(t, (Easings.EasingFunction)iFunction).RemapFrom01(m_movementXRange), iFunction * 3.0f, 0.0f); ;
        }
    }
}
