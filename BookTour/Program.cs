using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour
{
    class Program
    {
        static void Main(string[] args)
        {
            TravelAgency t = new TravelAgency("Tan Ah Huat Travel Far");

            t.Add(new Customer("Tan Lian Hwee", "Clementi Road", "C10010"));
            t.Add(new Customer("Lim Teck Gee", "Kent Ridge Road", "C10020"));
            t.Add(new Customer("Koh Ghim Moh", "Dover Road", "C10030"));
            t.Add(new Customer("Liat Kim Ho", "West Coast Road", "C10040"));

            t.Add(new Tour("Paris", 3400, 3));
            t.Add(new Tour("London", 3200, 3));
            t.Add(new Tour("Munich", 3100, 2));
            t.Add(new Tour("Milan", 3500, 3));
            TourPackage p = new TourPackage("Europe");
            p.ConsistOf(t.FindTour("London"));
            p.ConsistOf(t.FindTour("Paris"));
            t.Add(p);

            t.Add(new Trip(t.FindTour("Paris"), new DateTime(2015, 4, 2), 20));
            t.Add(new Trip(t.FindTour("Munich"), new DateTime(2015, 4, 8), 15));
            t.Add(new Trip(t.FindTour("Europe"), new DateTime(2015, 4, 12), 17));

            t.MakeBooking(t.FindCustomer("Lim Teck Gee"), t.FindTrip("Paris"), 7);
            t.MakeBooking(t.FindCustomer("Liat Kim Ho"), t.FindTrip("Europe"), 2);
            t.MakeBooking(t.FindCustomer("Koh Ghim Moh"), t.FindTrip("Munich"), 1);
            t.MakeBooking(t.FindCustomer("Tan Lian Hwee"), t.FindTrip("Europe"), 3);

            t.ListTours();
            t.ListTrips();
        }
    }
}
