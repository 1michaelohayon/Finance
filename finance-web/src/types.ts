export interface Liability {
  id: string;
  user: string;
  name: string;
  amount: number;
  reccuring?: boolean;
  active?: boolean;
  tags?: string[];
  interestRate?: number;
  minimumPayment?: number;
}

export type NewLiability = Omit<Liability, "id" | "user">;
