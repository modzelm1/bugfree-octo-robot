using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace StringSearchLogic.UnitTest
{
    /// <summary>
    /// OR have lower priority than AND
    /// whitespaces are AND by default
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void TestSimpleException()
        {
            var query = "(cat dog";
            var testData = "dog bird fish cat pig";

            var sm = new SearchManager();
            var fun = sm.BuildStringQueryFunction(query);
            var output = fun(testData);
        }

        [TestMethod]
        public void TestSimpleAND_Positive()
        {
            bool expectedOutput = true;

            //whitespaces are AND by default
            var query = "cat dog";
            var testData = "dog bird fish cat pig";

            var sm = new SearchManager();
            var fun = sm.BuildStringQueryFunction(query);
            var output = fun(testData);

            Assert.AreEqual(expectedOutput, output,
                assertFailMessage(query, expectedOutput.ToString(), output.ToString()));
        }

        [TestMethod]
        public void TestSimpleAND_Negative()
        {
            bool expectedOutput = false;

            //whitespaces are AND by default
            var query = "cat dog";
            var testData = "bird fish cat pig";

            var sm = new SearchManager();
            var fun = sm.BuildStringQueryFunction(query);
            var output = fun(testData);

            Assert.AreEqual(expectedOutput, output,
                assertFailMessage(query, expectedOutput.ToString(), output.ToString()));
        }

        [TestMethod]
        public void TestSimpleOR_Positive()
        {
            bool expectedOutput = true;

            var query = "cat OR dog";
            var testData = "bird fish cat pig";

            var sm = new SearchManager();
            var fun = sm.BuildStringQueryFunction(query);
            var output = fun(testData);

            Assert.AreEqual(expectedOutput, output,
                assertFailMessage(query, expectedOutput.ToString(), output.ToString()));
        }

        [TestMethod]
        public void TestSimpleOR_Negative()
        {
            bool expectedOutput = false;

            var query = "cat OR dog";
            var testData = "bird fish snake pig";

            var sm = new SearchManager();
            var fun = sm.BuildStringQueryFunction(query);
            var output = fun(testData);

            Assert.AreEqual(expectedOutput, output,
                assertFailMessage(query, expectedOutput.ToString(), output.ToString()));
        }

        [TestMethod]
        public void TestComplexQuery1_Positive()
        {
            bool expectedOutput = true;

            var query = " ( \"fish cat\"  pig) OR  dog"; //("fish cat" pig) OR dog
            var testData = "bird fish cat pig";

            var sm = new SearchManager();
            var fun = sm.BuildStringQueryFunction(query);
            var output = fun(testData);

            Assert.AreEqual(expectedOutput, output,
                assertFailMessage(query, expectedOutput.ToString(), output.ToString()));
        }

        [TestMethod]
        public void TestComplexQuery1_Negative()
        {
            bool expectedOutput = false;

            var query = " ( \"fish cat\"  pig) OR  dog"; //("fish cat" pig) OR dog
            var testData = "bird cat fish pig";

            var sm = new SearchManager();
            var fun = sm.BuildStringQueryFunction(query);
            var output = fun(testData);

            Assert.AreEqual(expectedOutput, output,
                assertFailMessage(query, expectedOutput.ToString(), output.ToString()));
        }

        [TestMethod]
        public void TestComplexQuery2_Positive()
        {
            bool expectedOutput = true;

            //("cat pig dog fly" OR cat AND tiger) snake fly (lion "tiger")
            //OR have lower priority than AND
            //whitespaces are AND by default
            var query = " ( \"cat pig dog fly\" OR cat AND tiger)  snake fly (lion \"tiger\")";
            var testData = "bird fish cat pig dog snake fly  lion tiger";

            var sm = new SearchManager();
            var fun = sm.BuildStringQueryFunction(query);
            var output = fun(testData);

            Assert.AreEqual(expectedOutput, output,
                assertFailMessage(query, expectedOutput.ToString(), output.ToString()));
        }

        [TestMethod]
        public void TestComplexQuery2_Negative()
        {
            bool expectedOutput = false;

            //("cat pig dog fly" OR cat AND tiger) snake fly (lion "tiger")
            //OR have lower priority than AND
            //whitespaces are AND by default
            var query = " ( \"cat pig dog fly\" OR wolf AND tiger)  snake fly (lion \"tiger\")";
            var testData = "bird fish cat pig dog snake fly  lion tiger";

            var sm = new SearchManager();
            var fun = sm.BuildStringQueryFunction(query);
            var output = fun(testData);

            Assert.AreEqual(expectedOutput, output,
                assertFailMessage(query, expectedOutput.ToString(), output.ToString()));
        }

        [TestMethod]
        public void TestComplexQuery3_Positive()
        {
            bool expectedOutput = true;

            var query = "fly AND (fish OR cat) AND snake";
            var testData = "bird fish cat pig dog snake fly  lion tiger";

            var sm = new SearchManager();
            var fun = sm.BuildStringQueryFunction(query);
            var output = fun(testData);

            Assert.AreEqual(expectedOutput, output,
                assertFailMessage(query, expectedOutput.ToString(), output.ToString()));
        }

        [TestMethod]
        public void TestComplexQuery3_Negative()
        {
            bool expectedOutput = false;

            var query = "ss AND (fish OR cat) AND snake";
            var testData = "bird fish cat pig dog snake fly  lion tiger";

            var sm = new SearchManager();
            var fun = sm.BuildStringQueryFunction(query);
            var output = fun(testData);

            Assert.AreEqual(expectedOutput, output,
                assertFailMessage(query, expectedOutput.ToString(), output.ToString()));
        }

        string assertFailMessage(string input, string expectedOutput, string output)
        {
            StringBuilder message = new StringBuilder(100);
            message.Append("for given input: ");
            message.Append(input);
            message.AppendLine();
            message.Append("output: ");
            message.Append(output);
            message.AppendLine();
            message.Append("does not match expected output: ");
            message.Append(expectedOutput);
            return message.ToString();
        }
    }
}
