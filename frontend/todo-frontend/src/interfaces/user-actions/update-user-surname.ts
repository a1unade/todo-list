import {UserActionTypes} from "../../types/actions/user-actions.ts";

interface IUpdateUserSurname {
    type: UserActionTypes.UPDATE_USER_SURNAME;
    // Новая фамилия пользователя
    payload: string;
}

export default IUpdateUserSurname;