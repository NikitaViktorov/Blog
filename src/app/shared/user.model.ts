import { Guid } from "guid-typescript";

export class User {
    public id: Guid;
    public Email: string;
    public Password: string;
    public Name: string;
    public Surname: string;
    public Role: string;
}
