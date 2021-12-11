export interface ApiResult<T>{
    data:T;
    errors:string[];
    succeeded:boolean;
}