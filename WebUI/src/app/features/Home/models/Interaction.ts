export interface Interaction{
    id : string,
    interactType :number,
    createdAt : Date,
    userId : string,
    quizzId: string
}
export interface CreateInteractionRequest{
    type: number,
    quizzId: string
}