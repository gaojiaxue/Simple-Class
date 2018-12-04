using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour
{
    //Parent class
    class Person
    {
        string name;
        string address;

        public Person(string name, string address)
        {
            this.name = name;
            this.address = address;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
    }
    //Child class
    class Customer : Person
    {
        string id;

        public Customer(string name, string address, string id)
            : base(name, address)
        {
            this.id = id;
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
    }
    //child class
    class TourGuide : Person
    {
        int salary;
        public TourGuide(string name, string address, int salary)
            : base(name, address)
        {
            this.salary = salary;
        }

        public int Salary
        {
            get { return salary; }
            set { salary = value; }
        }
    }
    //tour parent class
    class Tour
    {
        string name;
        int cost;
        int duration;
        List<string> poi;

        public Tour(string name, int cost, int duration, List<string> poi)
        {
            this.name = name;
            this.cost = cost;
            this.duration = duration;
            this.poi = poi;
        }

        public Tour(string city, int cost, int duration)
            : this(city, cost, duration, new List<string>())
        {
        }

        public void Add(string location)
        {
            poi.Add(location);
        }

        public string Name
        {
            get { return name; }
        }

        public virtual int Cost
        {
            get { return cost; }
        }

        public virtual int Duration
        {
            get { return duration; }
        }

        public override string ToString()
        {
            return String.Format("Tour:{0},{1} days,${2}",
                                 Name, Duration, Cost);
        }
    }
    //tour child class
    class TourPackage : Tour
    {
        List<Tour> places;

        public TourPackage(string t)
            : base(t, 0, 0, null)
        {
            places = new List<Tour>();
        }

        public void ConsistOf(Tour t)
        {
            places.Add(t);
        }

        public override int Cost
        {
            get
            {
                int c = 0;
                foreach (Tour t in places)
                    c = c + t.Cost;
                return (int)(c * 9 / 10); // 10% discount for tour package
            }
        }

        public override int Duration
        {
            get
            {
                int d = 0;
                foreach (Tour t in places)
                    d = d + t.Duration;
                return d;
            }
        }
    }
    //
    class TripOverBookedException : ApplicationException
    {
        public TripOverBookedException() : base() { }
        public TripOverBookedException(string message) : base(message) { }
    }
    //
    class Trip
    {
        Tour tour;
        DateTime when;
        int maxSize;
        List<Booking> bookings;

        public Trip(Tour tour, DateTime when, int maxsize)
        {
            this.tour = tour;
            this.when = when;
            this.maxSize = maxsize;
            bookings = new List<Booking>();
        }

        public Tour Tour
        {
            get { return tour; }
        }

        public int Numbers
        {
            get
            {
                int n = 0;
                foreach (Booking b in bookings)
                    n = n + b.Number;
                return n;
            }
        }

        public int GetRevenue()
        {
            int n = 0;
            foreach (Booking b in bookings)
                n = n + b.Cost;
            return n;
        }

        public void Book(Customer c, int number)
        {
            if (Numbers + number > maxSize)
                throw new TripOverBookedException();
            bookings.Add(new Booking(c, this, number));
        }

        public override string ToString()
        {
            return String.Format("Trip:{0},{1},{2},${3}",
                                 tour, when.ToShortDateString(),
                                 Numbers, GetRevenue());
        }
    }
    //
    class Booking
    {
        Customer customer;
        Trip trip;
        int number;

        public Booking(Customer customer, Trip trip, int number)
        {
            this.customer = customer;
            this.trip = trip;
            this.number = number;
        }

        public Booking(Customer customer, Trip trip)
            : this(customer, trip, 1)
        {
        }

        public Customer TheCustomer
        {
            get { return customer; }
        }

        public Trip TheTrip
        {
            get { return trip; }
        }

        public int Number
        {
            get { return number; }
        }

        public virtual int Cost
        {
            get
            {
                int c = trip.Tour.Cost * number;
                if (number > 5)
                    c = (int)(c * 0.95);
                // 5% discount for bookings of more than 5
                return c;
            }
        }
    }
    class TravelAgency
    {
        string name;
        List<Customer> customers;
        List<Tour> tours;
        List<Trip> trips;

        public TravelAgency(string name)
        {
            this.name = name;
            customers = new List<Customer>();
            tours = new List<Tour>();
            trips = new List<Trip>();
        }

        public void Add(Trip t)
        {
            trips.Add(t);
        }

        public void Add(Tour t)
        {
            tours.Add(t);
        }

        public void Add(Customer c)
        {
            customers.Add(c);
        }

        public Tour FindTour(string city)
        {
            foreach (Tour t in tours)
                if (t.Name.Equals(city))
                    return t;
            return null;
        }

        public Trip FindTrip(string city)
        {
            foreach (Trip t in trips)
                if (t.Tour.Name.Equals(city))
                    return t;
            return null;
        }

        public Customer FindCustomer(string name)
        {
            foreach (Customer c in customers)
            {
                if (c.Name.Equals(name))
                    return c;
            }
            return null;
        }

        public void MakeBooking(Customer c, Trip t, int n)
        {
            t.Book(c, n);
        }

        public void ListTrips()
        {
            foreach (Trip t in trips)
                Console.WriteLine(t);
        }

        public void ListTours()
        {
            foreach (Tour t in tours)
                Console.WriteLine(t);
        }
    }
}
