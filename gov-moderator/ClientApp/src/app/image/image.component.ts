import { Component, OnInit } from '@angular/core';
import { ModeratorApiService } from '../services/moderator-api.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-image',
  templateUrl: './image.component.html',
  styleUrls: ['./image.component.css']
})
export class ImageComponent implements OnInit {
  public image: any = {};
  public loading = false;

  constructor(private router: ActivatedRoute, private modApi: ModeratorApiService) { }

  ngOnInit() {
    this.router.params.subscribe(params => {
      this.loading = true;
      this.modApi.getImage(params.imageId).subscribe(data => {
        this.image = data;
        this.loading = false;
      });
    });
  }

  getBadgeClass(status) {
    switch (status) {
      case 'Approved':
        return 'badge-success';
      case 'Rejected':
        return 'badge-danger';
      case 'Pending':
        return 'badge-warning';
    }
  }
}
