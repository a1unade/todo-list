// Начальное состояние user state.
// Интерфейс для redux, хранит состояние пользователя в store.
interface IUserState {
    // Имя пользователя
    name: string,
    // Фамилия пользователя
    surname: string,
    // Дата рождения пользователя
    birthDate: string,
    // Пол
    gender: string,
    // Страна
    country: string, // Скорее всего лишнее, просто на будущее
    // Ник пользователя
    nickname: string,
    // Электронная почта пользователя
    email: string,
    // Выполнен ли вход
    isAuthenticated: boolean,
    // Аватарка пользователя
    avatar: string,
}

export default IUserState;