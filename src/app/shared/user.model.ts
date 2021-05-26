import { Guid } from "guid-typescript";

export class User {
    public id: Guid;
    public email: string;
    public password: string;
    public name: string;
    public surname: string;
    public role: string;
}
