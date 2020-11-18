namespace _02.VaniPlanning
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Agency : IAgency
    {
        Dictionary<string, Invoice> bySerial = new Dictionary<string, Invoice>();
        public void Create(Invoice invoice)
        {
            if (bySerial.ContainsKey(invoice.SerialNumber))
            {
                throw new ArgumentException();
            }

            bySerial[invoice.SerialNumber] = invoice;
        }

        public void ThrowInvoice(string number)
        {
            if (!bySerial.ContainsKey(number))
            {
                throw new ArgumentException();
            }

            bySerial.Remove(number);
        }

        public void ThrowPayed()
        {
            var toRemove = bySerial.Values.Where(x => x.Subtotal == 0).Select(x => x.SerialNumber).ToList();
            foreach (var item in toRemove)
            {
                bySerial.Remove(item);
            }
        }

        public int Count()
        {
            return bySerial.Count();
        }

        public bool Contains(string number)
        {
            return bySerial.ContainsKey(number);
        }

        public void PayInvoice(DateTime due)
        {
            var dueTo = bySerial.Values.Where(x => x.DueDate.Date == due.Date);
            if (dueTo.Count() == 0)
            {
                throw new ArgumentException();
            }

            foreach (var item in dueTo)
            {
                item.Subtotal = 0;
            }
        }

        public IEnumerable<Invoice> GetAllInvoiceInPeriod(DateTime start, DateTime end)
        {
            return bySerial.Values
                .Where(x => start.Date <= x.IssueDate.Date && x.IssueDate.Date <= end.Date)
                .OrderBy(x => x.IssueDate)
                .ThenBy(x => x.DueDate);

        }

        public IEnumerable<Invoice> SearchBySerialNumber(string serialNumber)
        {
            var keys = bySerial.Keys.Where(k => k.Contains(serialNumber));
            if (keys.Count() == 0)
            {
                throw new ArgumentException();
            }
            return keys.OrderByDescending(x => x).Select(x => bySerial[x]);
        }

        public IEnumerable<Invoice> ThrowInvoiceInPeriod(DateTime start, DateTime end)
        {
            var forRemove = bySerial.Values
                 .Where(i => start.Date < i.DueDate.Date && i.DueDate.Date < end.Date);

            if (forRemove.Count() == 0)
            {
                throw new ArgumentException();
            }

            foreach (var item in forRemove)
            {
                bySerial.Remove(item.SerialNumber);
            }

            return forRemove;
        }

        public IEnumerable<Invoice> GetAllFromDepartment(Department department)
        {
            return bySerial.Values.Where(i => i.Department == department)
                .OrderByDescending(i => i.Subtotal)
                .ThenBy(i => i.IssueDate);
        }

        public IEnumerable<Invoice> GetAllByCompany(string company)
        {

            return bySerial.Values.Where(i => i.CompanyName == company)
                .OrderByDescending(i => i.SerialNumber);
        }

        public void ExtendDeadline(DateTime dueDate, int days)
        {
            var forUpdate = bySerial.Values.Where(x => x.DueDate.Date == dueDate.Date);
            if (forUpdate.Count() == 0)
            {
                throw new ArgumentException();
            }
            foreach (var item in forUpdate)
            {
                item.DueDate = item.DueDate.AddDays(days);
            }
        }
    }
}
