export interface Author{
  authorModel: [
        {
          id: string,
          firstName: string,
          lastName: string,
          dateBirth?: Date,
          dateDeath?: Date,
          createDateTime: Date,
          updateDateTime: Date,
          isDeleted: boolean
        },
    ]
    messege: string,
    status: boolean,
    warning: string [],
    error: string [] 
}