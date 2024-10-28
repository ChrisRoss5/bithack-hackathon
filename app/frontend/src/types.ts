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
    pictureUrl?: string;
    createdAt: Date;
    contracts: ContractForSearch[];
  }
  interface ContractForSearch {
    userId: string;
    contractRanges: ContractRange[];
  }
  interface Contract {
    id: string;
    user: User;
    communityHome: Home;
    isFree: boolean;
    leasePurpose: string;
    usingCutlery: boolean;
    vat: number;
    status: ContractStatus;
    dateOfIssue: Date;
    contractRanges: ContractRange[];
  }
  interface ContractForm {
    contractRequest: {
      userId: string;
      communityHomeId: string;
      leasePurpose: string;
      isfree: boolean;
      usingCutlery: boolean;
    };
    contractRanges: ContractRange[];
  }
  enum ContractStatus {
    NEW,
    PREPARED,
    SIGNED_BY_MAYOR,
    SIGNED_BY_USER,
  }
  interface ContractRange {
    id?: string;
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
    firebaseUid?: string;
    id?: string;
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
    role: string;
  }
}

export const ROLE = Object.freeze({
  USER: "User",
  JANITOR: "Janitor",
  CITY_OFFICIAL: "CityOfficial",
  MAYOR: "Mayor",
});
