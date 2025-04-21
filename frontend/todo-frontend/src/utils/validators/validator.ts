import errors from './error-messages.ts';

export const validateEmail = (email: string) => {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (email.length === 0) {
        return errors.emptyEmail;
    } else if (!emailRegex.test(email)) {
        return errors.invalidEmail;
    }

    return '';
};

export const validateName = (name: string) => {
    const nameRegex = /^[a-zA-Z\u0400-\u04FF]+$/;
    if (name.length === 0) {
        return errors.emptyName;
    } else if (!nameRegex.test(name)) {
        return errors.invalidName;
    }
    return '';
};

export const validateSurname = (surname: string) => {
    const surnameRegex = /^[a-zA-Z\u0400-\u04FF]+$/;
    if (surname.length === 0) {
        return errors.emptySurname;
    } else if (!surnameRegex.test(surname)) {
        return errors.invalidSurname;
    }
    return '';
};

export const validateBirthDate = (value: string): string => {
    if (!value) {
        return errors.emptyDate;
    }

    const [day, month, year] = value.split('-').map(Number);

    if (!year || !month || !day) {
        return errors.invalidDate;
    }

    const date = new Date(year, month - 1, day);
    const isValidDate =
        date.getFullYear() === year &&
        date.getMonth() === month - 1 &&
        date.getDate() === day;

    if (!isValidDate) {
        return errors.invalidDate;
    }

    const currentDate = new Date();
    let age = currentDate.getFullYear() - year;

    if (currentDate.getMonth() + 1 < month || (currentDate.getMonth() + 1 === month && currentDate.getDate() < day)) {
        age--;
    }

    if (age < 14) {
        return errors.ageTooYoung;
    }

    return '';
};


export const validatePassword = (password: string) => {
    if (password.length < 8 || password.length > 12) {
        return errors.passwordLength;
    }

    const hasUpperCase = /[A-Z]/.test(password);
    const hasLowerCase = /[a-z]/.test(password);
    const hasDigits = /\d/.test(password);
    const hasSpecialChars = /[!@#$%]/.test(password);

    if (!hasUpperCase || !hasLowerCase || !hasDigits || !hasSpecialChars) {
        if (!hasUpperCase) return errors.passwordUpperCase;
        if (!hasLowerCase) return errors.passwordLowerCase;
        if (!hasDigits) return errors.passwordDigits;
        if (!hasSpecialChars) return errors.passwordSpecialChars;
    }

    return '';
};