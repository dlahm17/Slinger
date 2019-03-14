using System;
using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;

[RequireComponent (typeof (BillboardRenderer))]
public class SetupBillboardAsset : MonoBehaviour {

	public BillboardRenderer m_BillboardRenderer;
    [Space(5)]
    public bool EnableScript = true;
	[Space(5)]
	[Range(0.5f, 60.0f)]
	public float height = 1.0f;
	[Range(0.5f, 60.0f)]
	public float width = 1.0f;
	[Range(0.0f, -5.0f)]
	public float bottom = -0.1f;

    void Awake () {
        SetBillboardValues();
    }

	void OnValidate () {
        SetBillboardValues();
    }

    public void SetBillboardValues()
    {
        if (EnableScript)
        {
            if (m_BillboardRenderer == null)
            {
                m_BillboardRenderer = this.GetComponent<BillboardRenderer>();
            }
            if (m_BillboardRenderer.billboard != null)
            {
                m_BillboardRenderer.billboard.height = height;
                m_BillboardRenderer.billboard.width = width;
                m_BillboardRenderer.billboard.bottom = bottom;
            }
        }
    }
}
