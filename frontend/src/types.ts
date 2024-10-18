declare global {
  interface User {
    id: string;
    email: string;
    displayName: string;
    role: string;
  }

  interface Center {
    id: string;
    name: string;
    address: string;
    settlement: string;
    description: string;
    capacity: number;
    price: number;
    pictureUrl: string;
    latitude: number;
    longitude: number;
    createdAt: string;
  }

  interface Reservation {
    id: string;
    user: User;
    center: Center;
    reservationFrom: string;
    reservationTo: string;
    expectedNumberOfPeople: number;
    additionalNotes: string;
    createdAt: string;
  }
}
