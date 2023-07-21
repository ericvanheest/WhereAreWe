using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WhereAreWe;

namespace UnitTests_WhereAreWe
{
    [TestClass]
    public class TestPackedFiveBitValues
    {
        [TestMethod]
        public void GetBytes_WithBasicValues_ReturnsCorrectBytes()
        {
            int[] input = new int[] { 0, 5, 10, 20, 25, 31 };
            byte[] expected = new byte[] { 0xa0, 0x28, 0x34, 0x7f };
            byte[] result = PackedFiveBitValues.GetBytes(input);

            CollectionAssert.AreEqual(result, expected);
        }

        [TestMethod]
        public void New_WithBasicValues_ReturnsCorrectInts()
        {
            byte[] input = new byte[] { 0xEC, 0x3D, 0x72, 0x20 };
            int[] expected = new int[] { 12, 15, 15, 18, 3, 8 };
            int[] result = new PackedFiveBitValues(input, 0).Values;
            CollectionAssert.AreEqual(result, expected);
        }

        [TestMethod]
        public void New_WithBasicValues_ReturnsOriginalBytes()
        {
            byte[] input = new byte[4] { 0x12, 0x34, 0x56, 0x78 };
            int[] ints = new PackedFiveBitValues(input).Values;
            byte[] result = new PackedFiveBitValues(ints).Bytes;
            CollectionAssert.AreEqual(result, input);
        }

        [TestMethod]
        public void GetBytes_WithMinimumValues_ReturnsCorrectBytes()
        {
            int[] input = new int[] { 0, 0, 0, 0, 0, 0 };
            byte[] expected = new byte[] { 0x00, 0x00, 0x00, 0x00 };
            byte[] result = PackedFiveBitValues.GetBytes(input);

            CollectionAssert.AreEqual(result, expected);
        }

        [TestMethod]
        public void GetBytes_WithMaximumValues_ReturnsCorrectBytes()
        {
            int[] input = new int[] { 31, 31, 31, 31, 31, 31 };
            byte[] expected = new byte[] { 0xff, 0x7f, 0xff, 0x7f };
            byte[] result = PackedFiveBitValues.GetBytes(input);

            CollectionAssert.AreEqual(result, expected);
        }

        [TestMethod]
        public void SetBits_WithBasicValues_ReturnsCorrectLong()
        {
            PrivateType pt = new PrivateType(typeof(PackedFiveBitValues));
            long ret = (long) pt.InvokeStatic("SetBits", 0, 2, 1, 3, 5, 7, 9);
            Assert.AreEqual(ret, 10);
        }

        [TestMethod]
        public void IntFromBits_WithBasicValues_ReturnsCorrectInt()
        {
            PackedFiveBitValues p5b = new PackedFiveBitValues(0, 0, 0, 0, 0, 0);
            PrivateObject obj = new PrivateObject(p5b);
            int ret = (int)obj.Invoke("IntFromBits", 127, 1, 2, 3, 4, 5);
            Assert.AreEqual(ret, 31);
        }

        [TestMethod]
        public void GetBytes_WithNull_ReturnsZeroBytes()
        {
            byte[] result = PackedFiveBitValues.GetBytes(null);

            Assert.IsInstanceOfType(result, typeof(byte[]));
            Assert.IsTrue(result.Length == 0);
        }
    }
}
