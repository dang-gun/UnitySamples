using UnityEngine;

using StartSample;

public class AppStart : MonoBehaviour
{
    void Start()
    {
        SQLitePCLRaw.Startup.Setup();


        using(StartSample_SQLiteContext db1 = new StartSample_SQLiteContext())
        {
            db1.TestData.Add(new StartSampleDbModel() 
            { 
                Int1 = 11,
                Str1 = "11",
                Date1 = System.DateTime.MaxValue,
            });
        }
    }

    void Update()
    {

    }
}
