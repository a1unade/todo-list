import {UserActionTypes} from "../../types/actions/user-actions.ts";

interface IUpdateUserName {
    type: UserActionTypes.UPDATE_USER_NAME;
    // Новое имя пользователя
    payload: string;
}

export default IUpdateUserName;