using System;
using System.IO;
using Xunit;
using OrderBot;
using Microsoft.Data.Sqlite;

namespace OrderBot.tests
{
    public class OrderBotTest
    {
        public OrderBotTest()
        {
            using (var connection = new SqliteConnection(DB.GetConnectionString()))
            {
                connection.Open();

                var commandUpdate = connection.CreateCommand();
                commandUpdate.CommandText =
                @"
        DELETE FROM orders
    ";
                commandUpdate.ExecuteNonQuery();

            }
        }


        [Fact]
        public void TestWelcome()
        {
            Session oSession = new Session("12345");
            String sInput = oSession.OnMessage("hello")[0];
            Assert.Contains("Welcome", sInput);
        }

        [Fact]
        public void TestWelcomPerformance()
        {
            DateTime oStart = DateTime.Now;

            Session oSession = new Session("12345");
            String sInput = oSession.OnMessage("hello")[0];


            DateTime oFinished = DateTime.Now;
            long nElapsed = (oFinished - oStart).Ticks;
            System.Diagnostics.Debug.WriteLine("Elapsed Time: " + nElapsed);
            Assert.True(nElapsed < 10000);
        }


        [Fact]
        public void TestRoutines()
        {
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            oSession.OnMessage("Skin care");
            //oSession.OnMessage("Acne");
            String sInput = oSession.OnMessage("Acne")[0];
            Assert.Contains("routines", sInput);
        }

        [Fact]
        public void TestSkinType()
        {
            Session oSession = new Session("12345");

            oSession.OnMessage("hello");
            oSession.OnMessage("Skin care");
            oSession.OnMessage("Acne");
            oSession.OnMessage("Morning");

            String sInput = oSession.OnMessage("Hyaluronic Acid")[0];

            Assert.Contains("skin type", sInput);
        }


        [Fact]
        public void TestFullFlow()
        {
            Session oSession = new Session("12345");

            oSession.OnMessage("hello");
            oSession.OnMessage("Skin care");
            oSession.OnMessage("Acne");
            oSession.OnMessage("Morning");
            oSession.OnMessage("Hyaluronic Acid");
            oSession.OnMessage("Combination");
            oSession.OnMessage("No");
            oSession.OnMessage("60");
            oSession.OnMessage("DIY Masks");
            String sInput = oSession.OnMessage("Under-eye")[0];

            Assert.Contains("Your appoinment is booked", sInput);
        }

        [Fact]
        public void TestFullFlowPerfomance()
        {
            DateTime oStart = DateTime.Now;

            Session oSession = new Session("12345");

            oSession.OnMessage("hello");
            oSession.OnMessage("Skin care");
            oSession.OnMessage("Acne");
            oSession.OnMessage("Morning");
            oSession.OnMessage("Hyaluronic Acid");
            oSession.OnMessage("Combination");
            oSession.OnMessage("No");
            oSession.OnMessage("60");
            oSession.OnMessage("DIY Masks");
            String sInput = oSession.OnMessage("Under-eye")[0];

            //Assert.Contains("Your appoinment is booked", sInput);

            DateTime oFinished = DateTime.Now;
            long nElapsed = (oFinished - oStart).Ticks;
            System.Diagnostics.Debug.WriteLine("Elapsed Time: " + nElapsed);
            Assert.True(nElapsed < 10000);
        }

    }
}