import IUpdateUserName from "../../interfaces/user-actions/update-user-name.ts";
import IUpdateUserSurname from "../../interfaces/user-actions/update-user-surname.ts";
import ICreateUser from "../../interfaces/user-actions/create-user.ts";
import IUpdateUserNickname from "../../interfaces/user-actions/update-user-nickname.ts";
import IUpdateUserEmail from "../../interfaces/user-actions/update-user-email.ts";
import IUpdateUserAvatar from "../../interfaces/user-actions/update-user-avatar.ts";
import IDeleteUser from "../../interfaces/user-actions/delete-user.ts";

// Перечисление всех типов actions для user reducer
export enum UserActionTypes {
    UPDATE_USER_NAME = 'UPDATE_USER_NAME', // Обновление имени пользователя у redux-state
    UPDATE_USER_SURNAME = 'UPDATE_USER_SURNAME', // Обновление поля фамилии пользователя у redux-state
    CREATE_USER = 'CREATE_USER', // Создание redux-state пользователя при регистрации/входе
    UPDATE_USER_NICKNAME = 'UPDATE_USER_NICKNAME', // Обновление никнейма пользователя у redux-state
    UPDATE_USER_EMAIL = 'UPDATE_USER_EMAIL', // Обновление поля почты пользователя у redux-state
    UPDATE_USER_AVATAR = 'UPDATE_USER_AVATAR', // Смена аватарки у пользователя
    DELETE_USER = 'DELETE_USER', // Выход пользователя из аккаунта
}

export type UserAction =
    IUpdateUserName
    | IUpdateUserSurname
    | ICreateUser
    | IUpdateUserNickname
    | IUpdateUserEmail
    | IUpdateUserAvatar
    | IDeleteUser