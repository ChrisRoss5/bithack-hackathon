declare global {
  interface Home {
    id: string;
    name: string;
    rentPrice: number;
    address: string;
    leaseAmount: number;
    bailAmount: number;
    homeBills: number;
    vat: number;
    area: number;
    cutleryPrice?: number;
    capacity: number;
    createdAt: Date;
    contracts: Contract[];
  }
  interface ContractForSearch {
    userId: string;
    contractRanges: Range[];
  }
  interface Contract {
    id: string;
    user: User;
    home: Home;
    status: ContractStatus;
    dateOfIssue: Date;
    ranges: Range[];
  }
  interface ContractStatus {
    Prepared: "Prepared";
    MayorSigned: "MayorSigned";
  }
  interface Range {
    id: string;
    from: Date;
    to: Date;
  }
  interface UsageRecord {
    id: string;
    contract?: Contract;
    user?: User;
    conditionBefore: string;
    conditionAfter: string;
    damageDone: string;
    problems: string;
    createdAt: Date;
  }
  interface User {
    id: string;
    username: string;
    firstName?: string;
    lastName?: string;
    email: string;
    address?: string;
    city?: string;
    oib?: string;
    bank?: string;
    postalCode?: string;
    iban?: string;
    phoneNumber?: string;
    createdAt?: Date;
    role: Role;
  }
  enum Role {
    Superuser = "Superuser",
    Admin = "Admin",
    User = "User",
  }
}
