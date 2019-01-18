using Ivi.Visa;
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
        private Queue<string> simulatedReads, actualWrites = new Queue<string>();
        /// <summary>
        /// The actual mock session that should be passed to the driver under test
        /// </summary>
        public MockMessageBasedSession session = new MockMessageBasedSession();

        public ExpectMessageBasedSession()
        {
            session.WriteHandler = WriteHandler;
        }

        /// <summary>
        /// The strings in this queue are successively returned on each read
        /// On dispose, there must be no strings remaining.
        /// </summary>
        public Queue<string> SimulatedReads
        {
            get { return simulatedReads; }
            set {
                simulatedReads = value;
                session.ReadHandler = simulatedReads.Dequeue;
            }
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
                session.Dispose();
            // Ensure all messages were read
            Assert.AreEqual(0, simulatedReads.Count, "Not all simulated messages were read");
            // Ensure all expected writes happened
            Assert.AreEqual(0, ExpectedWrites.Count, "Not all expected messages were written");
        }
    }
}
