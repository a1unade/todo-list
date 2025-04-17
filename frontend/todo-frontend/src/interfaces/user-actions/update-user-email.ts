import {UserActionTypes} from "../../types/actions/user-actions.ts";

interface IUpdateUserEmail {
    type: UserActionTypes.UPDATE_USER_EMAIL;
    // Новая фамилия пользователя
    payload: string;
}

export default IUpdateUserEmail;