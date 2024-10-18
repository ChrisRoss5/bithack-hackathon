export interface DecodedJwt {
  aud?: string;
  exp?: number;
  iss?: string;
  [key: string]: string | number | undefined;
}

interface FormattedJwt {
  audience?: string;
  expiration?: number;
  issuer?: string;
  role?: Role;
  email?: string;
  name?: string;
  userId?: string;
}

const claimsMapping: { [key: string]: keyof FormattedJwt } = {
  "http://schemas.microsoft.com/ws/2008/06/identity/claims/role": "role",
  "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress": "email",
  "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name": "name",
  "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier":
    "userId",
};

export function formatDecodedData(decoded: DecodedJwt): FormattedJwt {
  const formattedData: FormattedJwt = {};

  for (const key in decoded) {
    if (claimsMapping[key]) {
      const mappedKey = claimsMapping[key];
      (formattedData as any)[mappedKey] = decoded[key] as string;
    } else if (key === "aud") {
      formattedData.audience = decoded[key] as string;
    } else if (key === "exp") {
      formattedData.expiration = decoded[key] as number;
    } else if (key === "iss") {
      formattedData.issuer = decoded[key] as string;
    } else {
      (formattedData as any)[key] = decoded[key];
    }
  }

  return formattedData;
}
