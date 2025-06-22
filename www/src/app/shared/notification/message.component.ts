import { TypeMessage } from "app/enums/TypeMessage";
import { ToastrService } from "ngx-toastr";

export class MessageComponent {
    constructor(private toastr: ToastrService,) {

    }

    showNotification(from: string, align: string, type: TypeMessage, message: string) {
        switch (type) {
            case TypeMessage.Info:
                this.toastr.info(
                '<span data-notify="icon" class="nc-icon nc-bell-55"></span><span data-notify="message">'+message+'</span>',
                "",
                {
                    timeOut: 4000,
                    closeButton: true,
                    enableHtml: true,
                    toastClass: "alert alert-info alert-with-icon",
                    positionClass: "toast-" + from + "-" + align
                }
                );
                break;
            case TypeMessage.Sucess:
                this.toastr.success(
                '<span data-notify="icon" class="nc-icon nc-bell-55"></span><span data-notify="message">'+message+'</span>',
                "",
                {
                    timeOut: 4000,
                    closeButton: true,
                    enableHtml: true,
                    toastClass: "alert alert-success alert-with-icon",
                    positionClass: "toast-" + from + "-" + align
                }
                );
                break;
            case TypeMessage.Warning:
                this.toastr.warning(
                '<span data-notify="icon" class="nc-icon nc-bell-55"></span><span data-notify="message">'+message+'</span>',
                "",
                {
                    timeOut: 4000,
                    closeButton: true,
                    enableHtml: true,
                    toastClass: "alert alert-warning alert-with-icon",
                    positionClass: "toast-" + from + "-" + align
                }
                );
                break;
            case TypeMessage.Error:
                this.toastr.error(
                '<span data-notify="icon" class="nc-icon nc-bell-55"></span><span data-notify="message">'+message+'</span>',
                "",
                {
                    timeOut: 4000,
                    enableHtml: true,
                    closeButton: true,
                    toastClass: "alert alert-danger alert-with-icon",
                    positionClass: "toast-" + from + "-" + align
                }
                );
                break;
            case TypeMessage.Show:
                this.toastr.show(
                '<span data-notify="icon" class="nc-icon nc-bell-55"></span><span data-notify="message">'+message+'</span>',
                "",
                {
                    timeOut: 4000,
                    closeButton: true,
                    enableHtml: true,
                    toastClass: "alert alert-primary alert-with-icon",
                    positionClass: "toast-" + from + "-" + align
                }
                );
                break;
            default:
                break;
        }
    }
}