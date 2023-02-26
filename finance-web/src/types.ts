export interface User {
  id: string;
  name: string;
  username: string;
  email: string;
  emailVerified: boolean;
  picture: string;
}

export interface Liability {
  id: string;
  user: User;
  name: string;
  amount: number;
  reccuring?: boolean;
  active?: boolean;
  tags?: string[];
  interestRate?: number;
  minimumPayment?: number;
}

export type NewLiability = Omit<Liability, "id">;
