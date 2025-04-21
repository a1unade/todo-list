
/// JWT токен каждого шага при регистрации
export interface RegistrationTokenPayload {
    /// Шаг регистрации
    Step: number;
    /// Данные, которые пользователь вводил
    Data: RegistrationTokenData | string;
    exp: number;
}

export interface RegistrationTokenData {
    /// Имя пользователя
    name?: string;
    /// Фамилия пользователя
    surname?: string;
    /// Дата рождения пользователя
    birthDate?: string;
    /// Пол пользователя
    gender?: 'M' | 'F';
    /// Почта пользователя
    email?: string;
    /// Пароль пользователя
    password?: string;
}