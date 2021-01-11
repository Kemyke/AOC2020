using System;

namespace AOC202025
{
    class Program
    {
        static BigInteger Compute(BigInteger subject, BigInteger loopSize)
        {
            return subject.modPow(loopSize, 20201227);
        }

        static void Main(string[] args)
        {
            BigInteger cardPubKey = 15113849;
            BigInteger doorPubKey = 4206373;


            for(long i = 1; i < 100000000; i++)
            {
                var e = Compute(7, i);
                if (e == doorPubKey || e == cardPubKey)
                {
                    //door loop 1245398
                    break;
                }
            }

            var retí1 = Compute(cardPubKey, 1245398);


            Console.WriteLine("Hello World!");
        }
    }
}
