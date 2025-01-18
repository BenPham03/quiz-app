import { Interaction } from "./Interaction";

export interface Quiz{
    id: string,
    description: string,
    status : string,
    title : string,
    config : string,
    interactions : Interaction[],
}
