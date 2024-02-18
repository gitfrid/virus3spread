namespace VirusSpreadLibrary.Creature.Rates;

public class PersBirthRateByAgeGroup
{
    public static double GetBirthRateByAgeGroup(int Age)
    {
        var AgeDeathRate = new List<(Func<int, bool> Key, double Value)>
        {
            (Key: x => x > 0  & x < 10 , Value: 0.70),
            (Key: x => x > 9 & x < 20 , Value: 0.71 ),
            (Key: x => x > 19 & x < 30 , Value: 0.72 ),
            (Key: x => x > 29 & x < 40 , Value: 0.73 ),
            (Key: x => x > 39 & x < 50 , Value: 0.74 ),
            (Key: x => x > 49 & x < 60 , Value: 0.75 ),
            (Key: x => x > 59 & x < 70 , Value: 0.76 ),
            (Key: x => x > 69 & x < 80 , Value: 0.77 ),
            (Key: x => x > 79 & x < 90 , Value: 0.78 ),
            (Key: x => x > 89 & x < 100 , Value: 0.79 ),
            (Key: x => x > 99 & x < 110 , Value: 0.80 ),
            (Key: x => x > 109 & x < 120 , Value: 0.81 ),
            (Key: x => x > 120 , Value: 1 )
        };
        return AgeDeathRate.SingleOrDefault(x => x.Key(Age)).Value;
    }

}
