export interface UserTable {
    userId: number;
    userName: string | null;
    userPassword: string | null;
    userAddress: string | null;
    mobileNumber: string | null;
    email: string | null;
    appointments: Appointment[];
}

export interface Appointment {
    userId: number | null;
    appointmentNumber: number;
    appointmentDate: Date | null;    
    appointmentTime: any | null;
    appointmentName: string | null;
    descriptions: string | null;
    user: UserTable | null;
}