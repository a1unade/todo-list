
/// JWT токен каждого шага при регистрации
export interface RegistrationTokenPayload {
    /// Шаг регистрации
    step: number;
    /// Данные, которые пользователь вводил
    data: {
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
    };
    exp: number;
}