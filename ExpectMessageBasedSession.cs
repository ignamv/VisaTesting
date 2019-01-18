﻿using Ivi.Visa;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisaTesting
{
    /// <summary>
    /// Test a mock Visa message-based session
    /// * The strings in SimulatedReads will be successively returned on each read.
    /// * On each write, take out an element from ExpectedWrites and check 
    ///   that it matches what was written.
    /// When disposed, check that SimulatedReads and ExpectedWrites are empty
    /// </summary>
    public class ExpectMessageBasedSession : IDisposable
    {
        /// <summary>
        /// The actual mock session that should be passed to the driver under test
        /// </summary>
        public MockMessageBasedSession session = new MockMessageBasedSession();

        public ExpectMessageBasedSession()
        {
            session.WriteHandler = WriteHandler;
            session.ReadHandler = ReadHandler;
        }

        /// <summary>
        /// The strings in this queue are successively returned on each read
        /// On dispose, there must be no strings remaining.
        /// </summary>
        public Queue<string> SimulatedReads { get; set; }

        private string ReadHandler()
        {
            if (SimulatedReads.Count == 0)
                return "";
            else
                return SimulatedReads.Dequeue();
        }

        private void WriteHandler(string msg)
        {
            string expected = ExpectedWrites.Dequeue();
            Assert.AreEqual(expected, msg, "Written message does not match expected");
        }

        /// <summary>
        /// The strings in this queue must match each successive write.
        /// On dipose, there must be no strings remaining.
        /// </summary>
        public Queue<string> ExpectedWrites { get; set; }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;
            disposed = true;
            if (disposing)
            {
                // Ensure all messages were read
                string expected = "end_terminator_120395776583764298357629837";
                SimulatedReads.Enqueue(expected);
                string actual = session.FormattedIO.ReadString();
                Assert.AreEqual(expected, actual, "Unread data remained in formatted IO buffer");
                Assert.AreEqual(0, SimulatedReads.Count, "Not all simulated messages were read");
                // Ensure all expected writes happened
                Assert.AreEqual(0, ExpectedWrites.Count, "Not all expected messages were written");
                session.Dispose();
            }
        }

        ~ExpectMessageBasedSession()
        {
            Dispose(false);
        }
    }
}
