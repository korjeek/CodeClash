export interface Response<T> {
    data: T;
    error: string;
    success: boolean;
}