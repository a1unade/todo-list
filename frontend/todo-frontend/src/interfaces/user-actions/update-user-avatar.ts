import {UserActionTypes} from "../../types/actions/user-actions.ts";

interface IUpdateUserAvatar {
    type: UserActionTypes.UPDATE_USER_AVATAR;
    // Новая аватарка пользователя
    payload: string;
}

export default IUpdateUserAvatar;