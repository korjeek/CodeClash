export interface JWTTokenProps {
    aud: string;
    exp: string;
    'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress': string;
    'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name': string;
    'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier': string;
    iss: string;
    jti: string;
}