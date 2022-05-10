export class Query {
    queryId: number=0;
    title!: string;
    content!: string;
    code: string="";
    isSolved: boolean=false;
    comments:QueryComment[]=[];
    constructor(){}
    
}

export class QueryComment{
        CommentId: number=0;
        message: string="";
        name:string="";
        code:string=""
        
    
}