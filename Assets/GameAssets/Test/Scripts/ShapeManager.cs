using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShapeManager : MonoBehaviour
{
    [SerializeField] private Shape[] _shapes;
    [SerializeField] private GameObject _finishText;

    // private int _enemiesFinishedAttacking;

    public void BeginTest()
    {
        // _finishText.SetActive(false);

        // await _shapes[0].RotateForSeconds(1 + 1 * 0);

        // // var tasks = new Task[_shapes.Length];
        // var tasks = new List<Task>();
        // for (int i = 1; i < _shapes.Length; i++)
        // {
        //     // StartCoroutine(_shapes[i].RotateForSeconds(1 + 1 * i));
        //     // await _shapes[i].RotateForSeconds(1 + 1 * i);
        //     // tasks[i] = _shapes[i].RotateForSeconds(1 + 1 * i);
        //     tasks.Add(_shapes[i].RotateForSeconds(1 + 1 * i));
        // }

        // await Task.WhenAll(tasks);
        // _finishText.SetActive(true);

        // var randomNumber = await GetRandomNumber();
        // var randomNumber = GetRandomNumber().GetAwaiter().GetResult();
        var randomNumber = GetRandomNumber().Result;

        print(randomNumber);
    }

    async Task<int> GetRandomNumber()
    {
        var randomNumber = Random.Range(100, 300);
        await Task.Delay(randomNumber);
        return randomNumber;
    }

    // IEnumerator Action1(Action callback)
    // {
    //     yield return new WaitForSeconds(1);
    //     _enemiesFinishedAttacking++;
    // }

    // IEnumerator Action2()
    // {
    //     yield return new WaitForSeconds(1);
    // }

    // IEnumerator Action3()
    // {
    //     yield return new WaitForSeconds(1);
    // }
}
