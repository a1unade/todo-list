import {UserActionTypes} from "../../types/actions/user-actions.ts";

interface IDeleteUser {
    type: UserActionTypes.DELETE_USER;
}

export default IDeleteUser;