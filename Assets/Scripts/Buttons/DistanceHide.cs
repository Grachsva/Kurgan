using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceHide : MonoBehaviour
{
    // ������ �������
    [SerializeField] private float frustumFieldOfView = 60.0f;  // ���� ������ �������
    [SerializeField] private float frustumAspect = 1.0f;        // ����������� ������
    [SerializeField] private float frustumNear = 0.3f;          // ��������� ���������
    [SerializeField] private float frustumFar = 10.0f;          // ������� ���������
    [SerializeField]
    private bool isInsideFrustum = false;

    private void OnDrawGizmos()
    {
        // ���������� ������ ��� ����������� � ���������
        Gizmos.color = Color.green;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.DrawFrustum(Vector3.zero, frustumFieldOfView, frustumFar, frustumNear, frustumAspect);
    }

    private void Update()
    {
        // ��������, ��������� �� ������ ������ �������
        isInsideFrustum = CheckIfInsideFrustum();

        // ��������� ���� ����������� Image, ����������� ������ ������� � ��� ��������
        Image[] images = GetComponentsInChildren<Image>();

        // ���� ������ ����������� �� ������
        if (images.Length > 0)
        {
            // ��������� ������������ � ����������� �� ����, ��������� �� ������ ������ �������
            float alpha = isInsideFrustum ? 1.0f : 0.0f;

            // ���������� ����� ������������ �� ���� ��������� ������������
            foreach (Image image in images)
            {
                Color color = image.color;
                color.a = alpha;
                image.color = color;
            }
        }
    }

    private bool CheckIfInsideFrustum()
    {
        // ���������� ���������� �� ������ �� ������ �������
        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 localCameraPosition = transform.InverseTransformPoint(cameraPosition);

        // ��������, ��������� �� ������ ������ ������� �� ����������
        if (localCameraPosition.z > frustumNear && localCameraPosition.z < frustumFar)
        {
            float halfHeightAtNear = Mathf.Tan(frustumFieldOfView * 0.5f * Mathf.Deg2Rad) * frustumNear;
            float halfWidthAtNear = halfHeightAtNear * frustumAspect;

            float halfHeightAtFar = Mathf.Tan(frustumFieldOfView * 0.5f * Mathf.Deg2Rad) * frustumFar;
            float halfWidthAtFar = halfHeightAtFar * frustumAspect;

            // �������� ������������ �������� ��� z ������� ������� ������
            float lerpFactor = (localCameraPosition.z - frustumNear) / (frustumFar - frustumNear);
            float currentHalfHeight = Mathf.Lerp(halfHeightAtNear, halfHeightAtFar, lerpFactor);
            float currentHalfWidth = Mathf.Lerp(halfWidthAtNear, halfWidthAtFar, lerpFactor);

            // ��������, �������� �� ������ � ������� ������� �������
            if (Mathf.Abs(localCameraPosition.x) < currentHalfWidth && Mathf.Abs(localCameraPosition.y) < currentHalfHeight)
            {
                return true;
            }
        }

        return false;
    }
}
