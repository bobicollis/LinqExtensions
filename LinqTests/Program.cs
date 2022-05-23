using System;
using System.Collections.Generic;
using System.Linq;
using LinqExtensions;

namespace LinqTests
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            List<Stuff> stuffList = new List<Stuff>
            {
                new Stuff
                {
                    StuffId = 1,
                    StuffName = "Stuff 1"
                },
                new Stuff
                {
                    StuffId = 2,
                    StuffName = "Stuff 2"
                },
                new Stuff
                {
                    StuffId = 3,
                    StuffName = "Stuff 3"
                }
            };

            var bitsList = new List<Bits>
            {
                new Bits
                {
                    FkStuffId = 1,
                    Data = "Bits Stuff 1"
                },
                new Bits
                {
                    FkStuffId = 2,
                    Data = "Bits Stuff 2_1"
                },
                new Bits
                {
                    FkStuffId = 2,
                    Data = "Bits Stuff 2_2"
                },
                new Bits
                {
                    FkStuffId = 2,
                    Data = "Bits Stuff 2_3"
                },
                new Bits
                {
                    FkStuffId = 4,
                    Data = "Bits Stuff 4"
                }
            };

            var groupedJoined = stuffList.LeftGroupJoin(bitsList, s => s.StuffId, b => b.FkStuffId, (s, b) => new StuffBits { Stuff = s, Bits = b });

            Console.WriteLine("Left Group Join");
            foreach (var j in groupedJoined)
            {
                if (j.Bits == null)
                {
                    Console.WriteLine($"joined stuff [{j.Stuff.StuffId} - {j.Stuff.StuffName}], with no bits");
                }
                else
                {
                    Console.WriteLine($"joined stuff [{j.Stuff.StuffId} - {j.Stuff.StuffName}], with {j.Bits.Count()} bits");
                }
            }

            var joined = stuffList.LeftJoin(bitsList, s => s.StuffId, b => b.FkStuffId, (s, b) => new StuffBit { Stuff = s, Bits = b });

            Console.WriteLine();
            Console.WriteLine("Left Join");
            foreach(var j in joined)
            {
                if (j.Bits == null)
                {
                    Console.WriteLine($"Joined stuff [{j.Stuff.StuffId} - {j.Stuff.StuffName}] with no bits");
                }
                else
                {
                    Console.WriteLine($"Joined stuff [{j.Stuff.StuffId} - {j.Stuff.StuffName}] with [{j.Bits.FkStuffId} - {j.Bits.Data}]");
                }
            }
        }
    }
}
