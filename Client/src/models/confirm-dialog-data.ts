export class ConfirmDialogData {
    title: string;
    message: string;
    confirmText: string;
    cancelText: string;
    constructor(title: string, message: string, confirmText: string,  cancelText: string){
        this.title = title;
        this.message = message;
        this.confirmText = confirmText;
        this.cancelText = cancelText;
    }
}