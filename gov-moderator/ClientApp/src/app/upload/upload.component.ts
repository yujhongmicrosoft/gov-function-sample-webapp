import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { FileUpload } from '../shared/file-upload';
import { ModeratorApiService } from '../services/moderator-api.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.css']
})
export class UploadComponent implements OnInit {
  public loading = false;
  public fileUpload = new FileUpload();
  @ViewChild('fileInput') fileInput;

  constructor(private router: Router , private modApi: ModeratorApiService, private element: ElementRef) { }

  ngOnInit() { }

  fileChange($event) {
    if ($event.target.files.length === 0) {
      return;
    }

    this.fileUpload.file = this.fileInput.nativeElement.files[0];

    let reader = new FileReader();
    let image = this.element.nativeElement.querySelector('.image-preview');
    reader.onload = function(e: any) {
      let src = e.target.result;
      image.src = src;
    };
    reader.readAsDataURL(this.fileUpload.file);
  }

  upload(){
    this.loading = true;
    this.modApi.upload(this.fileUpload).subscribe((data) => {
      this.loading = false;
      this.router.navigate(['/images', data.id]);
    });
  }
}

