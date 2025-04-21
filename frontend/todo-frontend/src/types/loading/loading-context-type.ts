export type LoadingContextType = {
    // Состояние загрузки
    loading: boolean;
    // Функция для включения загрузки
    startLoading: () => void;
    // Функция для выключения загрузки
    stopLoading: () => void;
}