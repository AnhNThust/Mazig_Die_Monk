using System.Threading.Tasks;
using UnityEngine;

public class Shape : MonoBehaviour
{
    // public IEnumerator RotateForSeconds(float duration)
    // {
    //     var end = Time.time + duration;
    //     while (Time.time < end)
    //     {
    //         transform.Rotate(new Vector3(1, 1) * Time.deltaTime * 150);
    //         yield return null;
    //     }
    // }

    public async Task RotateForSeconds(float duration)
    {
        var end = Time.time + duration;
        while (Time.time < end)
        {
            transform.Rotate(150 * Time.deltaTime * new Vector3(1, 1));

            await Task.Yield();
        }
    }
}
